import { createGlobalStyle } from 'styled-components';

export default createGlobalStyle `
	* {
		margin: 0;
		padding: 0;
		box-sizing: border-box;
	}

	body{
		background: ${props => props.theme.color.background};
		color: ${props => props.theme.color.text};
		font: 400 10px Roboto, sans-serif;	
	}

	button {
		background-color: ${props => props.theme.color.button};
		border: none;
		color: ${props => props.theme.color.text};
		padding: 15px 32px;		
		text-align: center;
		text-decoration: none;
		display: inline-block;
		font-size: 16px;		
	}
`;