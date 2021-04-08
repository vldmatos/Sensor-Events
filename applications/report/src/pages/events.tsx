import React, { Component } from "react";
import { Table } from '../styles/pages/home'

class Events extends Component {
	constructor(props) {
		super(props);
		this.state = {
			events: [],
			isLoaded: false
		}
	}

	componentDidMount(){
		fetch('http://localhost:4000/Event/latest')
			.then(res => res.json())
			.then(json => { this.setState({ isLoaded: true, events: json })})
			.catch(function(error) { console.log(error); });
	}

	render() {
		
		let { isLoaded, events } = this.state;

		if (!isLoaded) {
			return <div>Loading...</div>;	
		}
		
		return(
			
			<Table>
				<p>Last events received</p>	  
				{events.map(event => (
						
						<div key={event.id}>
							<label>Sensor: {event.sensor}</label>
							<label>Tag: {event.tag}</label>
							<label>Time: {event.time}</label>
							<label>Value: {event.value}</label>
							<label>Status: {event.status}</label>
						</div>
					
				))}; 
			</Table>
		);
	}
}

export default Events;
