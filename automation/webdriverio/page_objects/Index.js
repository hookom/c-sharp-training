var Page = require('./Base.js')

var IndexPage = Object.create(Page, {
    // Elements
    map: { get: function () { return browser.element('#map'); } },
    mapType: { get: function () { return browser.element("[title='Show satellite imagery']"); } },

    // Methods
    open: {
        value: function() {
            Page.open.call(this, '');
        }
    },
    mapIsVisible: {
        value: function() {
            return this.map.waitForVisible();
        }
    },
    mapTypeIsVisible: {
        value: function() {
            return this.mapType.waitForVisible();
        }
    },
});

module.exports = IndexPage;