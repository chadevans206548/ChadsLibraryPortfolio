import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-forbidden',
  standalone: true,
  imports: [],
  templateUrl: './forbidden.component.html',
  styleUrl: './forbidden.component.scss'
})
export class ForbiddenComponent implements OnInit {
  private returnUrl: string = '';

  constructor( private router: Router, private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  
  public navigateToLogin = () => {
    this.router.navigate(['/login'], { queryParams: { returnUrl: this.returnUrl }});
  }
}