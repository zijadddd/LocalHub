/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: "selector",
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        primary: {
          dark: "#17BF63",
          light: "#166F47",
          background: {
            100: "#15202B",
            200: "#131d27",
            300: "#111a22",
            400: "#0f161e",
            500: "#0d131a",
            600: "#0b1016",
            700: "#080d11",
            800: "#060a0d",
            900: "#020304",
          },
          divider: "#343434",
          warning: "#FF0000",
        },
        text: {
          primary: "#FFFFFD",
          secondary: "#87BF50",
          disabled: "#A3A3A3",
        },
        menu: {
          icon: "#6B7B87",
          selectedIcon: "#17BF63",
          textColor: "#FFFFFD",
          selectedText: "#17BF63",
        },
        label: {
          badgeBackground: "#575757",
          badgeText: "#8093a2",
          badgeRed: "#669fd5",
        },
        card: {
          background: "#192734",
        },
        toggles: {
          paperToggleButtonChecked: "#87BF50",
          paperToggleButtonCheckedBar: "#598330",
          paperToggleButtonUncheckedButton: "#A3A3A3",
          paperToggleButtonUncheckedBar: "#6F6F6F",
        },
      },
    },
  },
  plugins: [],
};
