import { Component, OnInit } from '@angular/core';
import { AuthorService } from '../../_services/author.service';
import { BookService } from '../../_services/book.service';

import { Book, Author } from '../../models';
import { FormGroup } from '@angular/forms';
import { ThrowStmt } from '@angular/compiler';
import { HttpClient } from '@angular/common/http';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  bookForm!: FormGroup; // ReactiveFormsModule
  books: Book[] = [];
  book: Book = this.newBook();
  message: string[] = [];
  authors: Author[] = [];

  constructor(
    private bookService: BookService,
    private authorService: AuthorService,
    private http:HttpClient
  ) { }

  ngOnInit(): void {
    this.getBooks();
    this.getAuthors();
  }

  newBook(): Book {
    return { id: 0, title: '', pages: 0, authorId: 0 }
  }

  cancel(): void {
    this.message = [];
    this.book = this.newBook();
    this.getBooks();
  }

  edit(book: Book): void {
    this.book = book;
    this.book.authorId = book.author ? book.author.id : 0;
    this.message = [];
  }

  getBooks(): void {
    this.bookService.getBooks()
      .subscribe(b => this.books = b);
  }

  getAuthors(): void {
    this.authorService.getAuthors()
      .subscribe(a => this.authors = a);
  }

  delete(book: Book): void {
    if (confirm('Er du sikker på du vil slette?')) {
      this.bookService.deleteBook(book.id)
        .subscribe(() => {
          this.books = this.books.filter(b => b.id != book.id)
        });
    }
  }

  save(): void {

    
    this.message = [];
    if (this.book.title == '') {
      this.message.push('Udfyld Titel');
    }

    if (this.book.pages < 1) {
      this.message.push('Udfyld Sider');
    }

    if (this.book.authorId < 1) {
      this.message.push('Vælg Forfatter');
    }

    if (this.message.length == 0) {
      if (this.book.id == 0) {

        const formData = new FormData();
        for (const key of Object.keys(this.bookForm.value)) {
          const value = this.bookForm.value[key];
          formData.append(key, value);
        }

        this.http.post<Book>('https://localhost:5001/api/book', formData).subscribe(res => console.log(res));

        // this.bookService.addBook(this.book)
        //   .subscribe(b => {
        //     this.books.push(b)
        //     this.book = this.newBook();
        //   });
      } else {
        this.bookService.updateBook(this.book.id, this.book)
          .subscribe(() => {
            this.book = this.newBook();
          });
      }
    }
  }
}
