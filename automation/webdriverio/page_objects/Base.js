function Page () {
    this.title = 'Base Page';
}

Page.prototype.open = function (path) {
    browser.url('/' + path);
}

module.exports = new Page();