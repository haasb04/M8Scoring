import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from "@angular/common";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/Rx';

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

    return this.getAuthFromServer(url, data);
  }

  refreshToken(): Observable<boolean> {
    var url = "api/token/auth";
    var data = {
      client_id: this.clientId,
      grant_type: "refresh_token",
      refresh_token: this.getAuth()!.refresh_token,
      scope: "offline_access profile email"
    };

    return this.getAuthFromServer(url, data);
  }

  //retrieve the access & refresh tokens from the server
  getAuthFromServer(url: string, data: any): Observable<boolean> {
    return this.http.post<TokenResponse>(url, data)
      .map((res) => {
        let token = res && res.token;
        //if token is there, login has been successful
        if (token) {
          //store username and jwt token
          this.setAuth(res);

          return true;
        }

        //login failed
        return Observable.throw('Unauthorized');
      })
      .catch(error => {
        return new Observable<any>(error);
      });
  }

  logout(): boolean {
    this.setAuth(null);
    return true;
  }

  setAuth(auth: TokenResponse | null): boolean {
    if (isPlatformBrowser(this.platformId)) {
      if (auth) {
        localStorage.setItem(this.authKey, JSON.stringify(auth));
      } else {
        localStorage.removeItem(this.authKey);
      }
    }
    return true;
  }

  getAuth(): TokenResponse | null {
    if (isPlatformBrowser(this.platformId)) {
      var i = localStorage.getItem(this.authKey);
      if (i) {
        return JSON.parse(i);
      }
    }
    return null;
  }

  isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem(this.authKey) != null;
    }

    return false;
  }

  isInRole(role: string) {
    let ts: TokenResponse = this.getAuth();
    if (ts) {
      return ts.roles.indexOf(role) >= 0;
    } else {
      return false;
    }
  }
}
