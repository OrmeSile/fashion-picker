import {Component, inject, OnInit, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {environment} from '../environments/environment';
import {Navbar} from './components/shared/navbar/navbar';
import {AuthApi} from './services/api/auth-api/auth-api';
import {UserStore} from './stores/user-store/user.store';

@Component({
  selector: 'fp-root',
  imports: [RouterOutlet, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  protected readonly title = signal('Web');
  protected readonly environment = environment;
  private readonly authApi = inject(AuthApi);
  private readonly userStore = inject(UserStore);

  ngOnInit() {
    this.authApi.getUserInformation()
      .subscribe(res => this.userStore.dispatch({type: 'SET_USER', payload: res}));
  }
}
