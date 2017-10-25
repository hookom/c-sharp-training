var selenium = require('selenium-webdriver'),
    By = selenium.By,
    until = selenium.until;
 
IndexPage = function IndexPage(driver) {
    this.driver = driver;
    this.url = 'http://climbontheway.com';

    // Element Selectors
    this.mapSelector = By.id("map");
};
 
IndexPage.prototype.visit = function() {
    this.driver.get(this.url);
    return selenium.promise.fulfilled(true);
};

IndexPage.prototype.mapContainerPresent = function() {
    return this.driver.findElement(this.mapSelector)
        .then(function() {
            return true;
        });
};

IndexPage.prototype.getMapContainer = function() {
    return this.driver.findElement(this.mapSelector)
        .then(function(el) {
            return el;
        });
};
 
module.exports = IndexPage;