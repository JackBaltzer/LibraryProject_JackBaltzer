import { Component, OnInit } from '@angular/core';

import { AuthorService } from '../../_services/author.service';
import { Author } from '../../models';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})
export class AuthorComponent implements OnInit {

  authors: Author[] = [];
  author: Author = this.newAuthor();
  message: string[] = [];

  constructor(
    private authorService: AuthorService
  ) { }

  ngOnInit(): void {
    this.getAuthors();
  }

  newAuthor(): Author {
    return { id: 0, firstName: '', lastName: '', middleName: '', books: [] };
  }

  getAuthors(): void {
    this.authorService.getAuthors()
      .subscribe(a => this.authors = a);
  }

  edit(author: Author): void {
    this.author = author;
    this.message = [];
  }

  delete(author: Author): void {
    if (confirm('Er du sikker på du vil slette?')) {
      this.authorService.deleteAuthor(author.id)
        .subscribe(() => {
          this.authors = this.authors.filter(a => a.id != author.id)
        });
    }
  }

  cancel(): void {
    this.message = [];
    this.author = this.newAuthor();
    this.getAuthors();
  }

  save(): void {
    this.message = [];
    if (this.author.firstName == '') {
      this.message.push('Udfyld Fornavn');
    }
    if (this.author.lastName == '') {
      this.message.push('Udfyld Efternavn');
    }
    if (this.message.length == 0) {
      if (this.author.id == 0) {
        this.authorService.addAuthor(this.author)
          .subscribe(a => {
            this.authors.push(a)
            this.author = this.newAuthor();
          });
      } else {
        this.authorService.updateAuthor(this.author.id, this.author)
          .subscribe(() => {
            this.author = this.newAuthor();
          });
      }
    }
  }
}
