import { describe, expect, it } from 'vitest';
import { buildApiUrl, buildWebSocketUrl } from './gatewayClient';

describe('gateway URL construction', () => {
  it('uses relative API paths by default', () => {
    expect(buildApiUrl('', '/api/robots')).toBe('/api/robots');
  });

  it('normalizes a configured gateway trailing slash', () => {
    expect(buildApiUrl('https://gateway.example///', '/api/robots')).toBe('https://gateway.example/api/robots');
  });

  it('does not create a duplicate slash before API routes', () => {
    expect(buildApiUrl('https://gateway.example/', 'api/robots')).toBe('https://gateway.example/api/robots');
  });

  it('uses the current browser origin for same-origin WebSockets', () => {
    expect(buildWebSocketUrl('', '/ws/robots/robot%2F1/live', 'http://localhost:5174/').toString())
      .toBe('ws://localhost:5174/ws/robots/robot%2F1/live');
  });

  it('maps an HTTP gateway override to ws', () => {
    expect(buildWebSocketUrl('http://gateway.example/', '/ws/robotics/live').toString())
      .toBe('ws://gateway.example/ws/robotics/live');
  });

  it('maps an HTTPS gateway override to wss', () => {
    expect(buildWebSocketUrl('https://gateway.example/', '/ws/robotics/live').toString())
      .toBe('wss://gateway.example/ws/robotics/live');
  });
});
