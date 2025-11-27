import { defineConfig } from 'vitest/config';
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
    test: {
    environment: 'jsdom',               // gives you document/window
    globals: true,                      // allows using describe/it/expect without imports
    setupFiles: './src/setupTests.ts',  // load jest-dom matchers
  },
})
