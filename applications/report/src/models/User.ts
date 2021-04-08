class User
{
	token: string;
	id: number;
	username: string;	

	constructor(toke: string, id: number, username: string)
	{
		this.token = toke;
		this.id = id;
		this.username = username;
	}
}