import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthService {
  authKey: string = "auth";
  clientId: string = "M8Scoring";

  constructor(private http: HttpClient, @Inject(PLATFORM_ID) private platformId: any) {

  }

  login(username: string, password: string): Observable<boolean> {
    var url = "api/token/auth";
    var data = {
      username: username,
      password: password,
      client_id: this.clientId,
      grant_type: "password",
      scope: "offline_access profile email"
    };

    return Observable.of(true); 

    //return this.http.post<TokenResponse>(url, data)
    //  .map((result) => {
    //    let token = result && result.token;
    //    //if the token is there, login has been successful
    //    if (token) {
    //      this.setAuth(result);
    //      return true;
    //    }

    //    //failed login
    //    return Observable.throw('Unauthorized');
    //  })
    //  .catch(error => {
    //    return new Observable<any>(error);
    //  });
  }

  logout(): boolean {
    this.setAuth(null);
    return true;
  }

  setAuth(auth: TokenResponse | null): boolean {
    if (auth) {
      localStorage.setItem(this.authKey, JSON.stringify(auth));
    } else {
      localStorage.removeItem(this.authKey);
    }
    return true;
  }

  getAuth(): TokenResponse | null {
    var i = localStorage.getItem(this.authKey);
    if (i) {
      return JSON.parse(i);
    } else {
      return null;
    }
  }

  isLoggedIn(): boolean {
    //return localStorage.getItem(this.authKey) != null;
    return true;
  }
}
