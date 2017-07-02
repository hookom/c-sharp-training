#!/usr/bin/perl

use strict;
use warnings;
use Data::Dumper;
use DateTime;
#use JSON;

my $file_name  = $ARGV[0] or die "CSV file name not supplied\n";
my $run_date   = $ARGV[1] or die "Run date not supplied\n";

my @supplied_run_date  = split("-", $run_date);

my $run_dt = DateTime->new(
    year  => $supplied_run_date[0],
    month => $supplied_run_date[1],
    day   => $supplied_run_date[2],
);

open (my $fh, '<', $file_name) or die "Could not open CSV file";

# hash of hash of scalars
# {
#   supervisor_id => {
#                      employee_id => hire_date,
#                      employee_id => hire_date,
#                      ...
#                    },
#   supervisor_id => {
#                     employee_id => hire_date,
#                     employee_id => hire_date,
#                      ...
#                    },
#   ...
# }

my %anniversaries = ();
while (my $line = <$fh>) {
    if ($line =~ /employee_id/) {
        next;
    }

    chomp($line);

    my @fields = split(",", $line);

    my @hire_date = split("-", $fields[3]);

    my $first_anniversary = $hire_date[0] + 5;
    my $anni_dt = DateTime->new(
        year  => $first_anniversary,
        month => $hire_date[1],
        day   => $hire_date[2],
    );

    # These anniversaries _can_ be the same as the run date
    while (DateTime->compare( $anni_dt, $run_dt ) < 0) {
        $first_anniversary += 5;
        $anni_dt = DateTime->new(
            year  => $first_anniversary,
            month => $hire_date[1],
            day   => $hire_date[2],
        );
    }

    # last field (pop) is the supervisor_id
    # first field (shift) is the employee_id
    $anniversaries{pop(@fields)}{shift(@fields)} = $anni_dt->ymd();
}

#foreach my $super (keys %anniversaries) {
#    my @milestones = ();
#    foreach my $employee (keys $anniversaries{$super}) {
#        my %employee_anni_data;
#        $employee_anni_data{"employee_id"} = $employee;
#        $employee_anni_data{"anniversary_date"} = $anniversaries{$super}{$employee};
#        push(@milestones, %employee_anni_data);
#    }

#    my %output;
#    $output{"supervisor_id"} = $super;
#    $output{"upcoming_milestones"} = @milestones;

#    my $json = encode_json(\%output);
#    print("$json\n\n");
#}

print(Dumper(%anniversaries));

close($fh);
