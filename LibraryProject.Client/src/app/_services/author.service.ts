import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { Author } from '../models';

import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  apiUrl = environment.apiUrl + '/author';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient
  ) { }

  getAuthors(): Observable<Author[]> {
    return this.http.get<Author[]>(this.apiUrl)
  }

  getAuthor(authorId: number): Observable<Author> {
    return this.http.get<Author>(`${this.apiUrl}/${authorId}`);
  }

  addAuthor(author: Author): Observable<Author> {
    return this.http.post<Author>(this.apiUrl, author, this.httpOptions);
  }

  updateAuthor(authorId: number, author: Author): Observable<Author> {
    return this.http.put<Author>(`${this.apiUrl}/${authorId}`, author, this.httpOptions);
  }

  deleteAuthor(authorId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${authorId}`, this.httpOptions);
  }
}
