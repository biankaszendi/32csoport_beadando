import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {
  private socket: WebSocket;

  constructor() { }

  connect(url: string): Observable<MessageEvent> {
    this.socket = new WebSocket(url);

    // Wrap WebSocket events in Observables
    return new Observable(observer => {
      this.socket.onmessage = (event) => observer.next(event);
      this.socket.onerror = (event) => observer.error(event);
      this.socket.onclose = () => observer.complete();
    });
  }

  sendMessage(message: string): void {
    this.socket.send(message);
  }

  close(): void {
    this.socket.close();
  }
}