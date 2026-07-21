import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

const gatewayTarget = 'http://localhost:5080';

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/health': {
        target: gatewayTarget,
        changeOrigin: true,
      },
      '/api': {
        target: gatewayTarget,
        changeOrigin: true,
      },
      '/ws': {
        target: gatewayTarget,
        changeOrigin: true,
        ws: true,
      },
    },
  },
});
