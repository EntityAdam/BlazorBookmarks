const plugin = require('tailwindcss/plugin')

module.exports = {
    content: [
        '**/*.html',
        '**/*.razor',
        './node_modules/tw-elements/dist/js/**/*.js'
    ],
    theme: {
        extend: {},
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('tw-elements/dist/plugin')
    ]
}