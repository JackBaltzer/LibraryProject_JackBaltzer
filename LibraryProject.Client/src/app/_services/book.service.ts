import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Book } from '../models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  apiUrl = environment.apiUrl + '/book';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient
  ) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl)
  }
  getBook(bookId: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${bookId}`);
  }

  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book, this.httpOptions);
  }

  updateBook(bookId: number, book: Book): Observable<Book> {
    return this.http.put<Book>(`${this.apiUrl}/${bookId}`, book, this.httpOptions);
  }

  deleteBook(bookId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${bookId}`, this.httpOptions);
  }
}
