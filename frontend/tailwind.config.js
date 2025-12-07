/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'campus-green': '#007f42',
        'campus-blue': '#29338a',
        'campus-green-dark': '#006634', // A slightly darker shade for hover states
        'campus-blue-dark': '#1f2666',  // A slightly darker shade for hover states
      },
    },
  },
  plugins: [],
}

