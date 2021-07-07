import { Component, OnInit } from '@angular/core';

import { Author } from '../models';
import { AuthorService } from '../author.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  authors: Author[] = [];

  constructor(
    private authorService: AuthorService
  ) { }

  ngOnInit(): void {
    this.authorService.getAuthors()
      .subscribe(a => this.authors = a);
  }
}
