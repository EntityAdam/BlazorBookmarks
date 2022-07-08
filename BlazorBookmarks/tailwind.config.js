const plugin = require('tailwindcss/plugin')

module.exports = {
    content: [
        '**/*.html',
        '**/*.razor',
    ],
    theme: {
        extend: {},
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/forms'),
    ]
}