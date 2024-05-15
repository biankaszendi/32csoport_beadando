import { Component } from '@angular/core';
import { Router, RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { WebsocketService } from './websocket.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements ngOnInit {
  title = 'BlogBeadandoClient';
constructor(private websocketService: WebsocketService) { }

  ngOnInit(): void {
    this.websocketService.connect('ws://localhost:8080').subscribe(
      (message: MessageEvent) => {
        console.log('Received message:', message.data);
      },
      (error) => {
        console.error('WebSocket error:', error);
      },
      () => {
        console.log('WebSocket connection closed');
      }
    );
  }

  sendMessage(): void {
    this.websocketService.sendMessage('Hello WebSocket!');
  }

  closeConnection(): void {
    this.websocketService.close();
}
