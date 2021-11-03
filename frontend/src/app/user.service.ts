import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public getUsers() {
    return this.http.get("https://jsonplaceholder.typicode.com/users");
  }

  public getUser(userId: any) {
    return this.http.get("https://jsonplaceholder.typicode.com/users/" + userId);
  }

  public createUser(body: any) {
    return this.http.post("https://jsonplaceholder.typicode.com/users", body);
  }

  public updateUser(userId: number | undefined, body: any) {
    return this.http.put("https://jsonplaceholder.typicode.com/users/" + userId, body);
  }

  public deleteUser(userId: number | undefined) {
    return this.http.delete("https://jsonplaceholder.typicode.com/users/" + userId);
  }
}
