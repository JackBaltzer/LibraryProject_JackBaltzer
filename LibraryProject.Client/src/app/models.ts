export interface Author {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  books: Book[]
}

export interface Book {
  id: number;
  title: string;
  pages: number;
  authorId: number;
  author?: Author;
}

export interface User {
  id: number;
  username: string;
  email: string;
  token?: string;
}
