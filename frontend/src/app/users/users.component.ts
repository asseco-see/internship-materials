import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { SimpleDialogComponent } from '../simple-dialog/simple-dialog.component';
import { UserService } from '../user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'name', 'username', 'phone', 'action'];
  public dataSource = new MatTableDataSource();
  constructor(
    private userService: UserService,
    private router: Router,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((users: any) => {
      this.dataSource.data = users;
    });
  }

  public showUserDetails(userId: number) {
    this.router.navigate(['users/' + userId]);
  }

  public removeUser(userId: number) {
    const dialogRef = this.dialog.open(SimpleDialogComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.userService.deleteUser(userId).subscribe((res) => {
          const array = JSON.parse(JSON.stringify(this.dataSource.data));
          const index = array.findIndex((user: any) => user.id === userId);
          console.log('index', index);
          array.splice(index, 1);
          console.log(array);
          this.dataSource.data = array;
        })
      }
    });
  }

  public addUser() {
    console.log('addUser');
    this.router.navigate(['users/add-user']);
  }

}
