import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../utils/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  user$ = this.authService.state$;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

}
