import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { TableRow } from '../../models/tablerow';


const maxTableSize: Number = 10;

@Component({
	selector: 'home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

	hub: HubConnection | undefined;
	data = new Array<TableRow>();

	isButtonStarted: boolean = false;
	buttonCaption: string = "Start";

	ngOnInit(): void {

		debugger;
		this.hub = new signalR.HubConnectionBuilder()
			.withUrl('/dataXchangeHub')
			.configureLogging(signalR.LogLevel.Information)
			.build();

		this.hub.start().catch(err => console.error(err.toString()));

		this.hub.on('ReceiveMessage', this.onMessage.bind(this));
	}

	startStopSpamming() {
		this.isButtonStarted = !this.isButtonStarted;
		this.buttonCaption = this.isButtonStarted ? "Stop" : "Start";

		if (this.hub)
			this.hub.send('StartStopSpamming');
	}

	private onMessage(name: string, description: string) {

		if (this.data.length >= maxTableSize) {
			this.data.shift();
		}

		this.data.push(new TableRow(name, description));
	}
}
