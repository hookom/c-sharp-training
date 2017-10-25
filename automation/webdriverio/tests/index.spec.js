var assert = require('chai').assert;
var LoginPage = require('../page_objects/Index.js');

describe('Index Page Tests', ()=> {

    it('Title should be "Climb on the Way"', ()=> {
        LoginPage.open();
        var title = browser.getTitle();
        assert.equal(title, 'Climb on the Way');
    });

    it('Map is displayed', ()=> {
        assert.isTrue(LoginPage.mapIsVisible());
    });

    it('MapType element is displayed', ()=> {
        assert.isTrue(LoginPage.mapTypeIsVisible());
    });

});