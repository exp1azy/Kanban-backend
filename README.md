# KanbanBackend
Kanban board implemented with ASP.NET using PostgreSQL. The client part is written in React.js by [Evgeny](https://github.com/e-zybkin)
## Requests
### Users
1) POST - add new user
2) POST - authenticate user
### Boards
1) GET - get all users boards
2) POST - add new board
3) PUT - update board by id
4) DELETE - delete board by id
### Board columns
1) GET - get all columns by board id
2) POST - add new column in the board by its id
3) PUT - update column by id
4) DELETE - delete column by id
### Cards
1) GET - get all cards from a column by its id
2) POST - add new card in the column by its id
3) PUT - update card name by id
4) PUT - update card text by id
5) DELETE - delete card by id
