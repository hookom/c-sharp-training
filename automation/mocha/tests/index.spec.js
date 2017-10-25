// Base dependecies
var assert = require('chai').assert,
    test = require('selenium-webdriver/testing'),
    selenium = require('selenium-webdriver');

// Chromedriver-specific dependencies/setup
var chrome = require('selenium-webdriver/chrome'),
    path = require('chromedriver').path,
    service = new chrome.ServiceBuilder(path).build();
chrome.setDefaultService(service);

// Page Object dependencies
var IndexPage = require('../page_objects/IndexPage.js');

test.describe('Index Page Tests', function () {
    this.timeout(10000);
    var driver;
    beforeEach("Build webdriver", function() {
        driver = new selenium.Builder()
            .withCapabilities(selenium.Capabilities.chrome())
            .build();
    });

    test.it('Title should be "Climb on the Way"', function () {
        var indexPage = new IndexPage(driver);
        indexPage.visit();

        driver.executeScript('return document.title')
            .then(function(return_value) {
                assert.equal(return_value, 'Climb on the Way', "Title did not match")
            });
    });

    test.it('Map container should be present', function () {
        var indexPage = new IndexPage(driver);
        indexPage.visit();

        indexPage.mapContainerPresent()
            .then(function(presence) {
                assert.isTrue(presence);
            });
    });

    test.it('Map container should have proper id value', function () {
        var indexPage = new IndexPage(driver);
        indexPage.visit();

        indexPage.getMapContainer()
            .then(function(el) {
                el.getAttribute("id")
                    .then(function(id) {
                        assert.equal(id, "map");
                    })
            });
    });

    afterEach("Quit webdriver", function() {
        driver.quit();
    });
});