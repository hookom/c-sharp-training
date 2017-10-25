module.exports = {
    url: 'http://www.climbontheway.com',
    elements: {
        map: {
            selector: 'div[id=map]'
        },
        originMarker: 'img[src*="&text=A"]',
        destMarker:   'img[src*="&text=B"]',
        fromInput:    'input[id=from]',
        toInput:      'input[id=to]',
        distInput:    'input[id=distance]',
        submitBtn:    'input[type=submit]',
    }
}