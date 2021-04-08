import React from 'react'
import Head from 'next/head'

import SensorLogo from '../assets/sensor.svg'
import { Container } from '../styles/pages/home'

import Events from '../pages/events'

const Home: React.FC = () => {
  return (
    <Container>
    	<Head>
        	<title>Report Events Sensors</title>
      	</Head>
      	<SensorLogo/>

	  	<h1>Events Sensors</h1>
	 	<Events/>
	</Container>
  )
}

export default Home