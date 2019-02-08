import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './auth.service';
import { Subject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  auth: AuthService;
  refreshTokenInProgress: boolean;

  tokenRefreshedSource = new Subject();
  tokenRefreshed$ = this.tokenRefreshedSource.asObservable();


  constructor(private injector: Injector, private router:Router) { }

  addAuthHeader(request:HttpRequest<any>) {
    var token = (this.auth.isLoggedIn()) ? this.auth.getAuth()!.token : null;

    if (token) {
      return request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    } else {
      return request;
    }
  }

  refreshToken() {
    if (this.refreshTokenInProgress) {
      return new Observable(observer => {
        this.tokenRefreshed$.subscribe(() => {
          observer.next();
          observer.complete();
        });
      });
    } else {
      this.refreshTokenInProgress = true;

      return this.auth.refreshToken()
        .do(() => {
          this.refreshTokenInProgress = false;
          this.tokenRefreshedSource.next();
        });
    }
  }

  logout() {
    this.auth.logout();
    this.router.navigate(["login"]);
  }


  intercept(
    request: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

    this.auth = this.injector.get(AuthService);

    //Handle request
    request = this.addAuthHeader(request);

    //Handle response
    return next.handle(request).catch(error => {
      if (error.status === 401) {
        console.log("Refreshing Token...");
        return this.refreshToken()
          .switchMap(() => {
            request = this.addAuthHeader(request);
            return next.handle(request);
          })
          .catch(() => {
            this.logout();
            return Observable.empty();
          });
      }

      return Observable.throw(error);
    });
  }
}
