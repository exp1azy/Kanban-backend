create table if not exists kanban_user (
	id serial not null primary key,
	name varchar(255) not null,
	email varchar(255) not null,
	password varchar(255) not null
);
	
create table if not exists kanban_board (
	id serial not null primary key,
	user_id int not null references kanban_user(id),
	name varchar(255) not null
);

create table if not exists board_column (
	id serial not null primary key,
	board_id int not null references kanban_board(id),
	name varchar(255) not null,
	position int not null
);

create table if not exists board_card (
	id serial not null primary key,
	column_id int not null references board_column(id),
	name varchar(255) not null,
	content text
);