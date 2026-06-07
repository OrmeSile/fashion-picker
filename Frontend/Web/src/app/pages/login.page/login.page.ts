import {Component, signal, viewChild} from '@angular/core';
import {form, FormField} from '@angular/forms/signals';
import {EyeOpenIcon} from '../../components/icons/eye-open.icon/eye-open.icon';
import {EyeClosedIcon} from '../../components/icons/eye-closed.icon/eye-closed.icon';

@Component({
  selector: 'fp-login.page',
  imports: [
    FormField,
    EyeOpenIcon,
    EyeClosedIcon
  ],
  templateUrl: './login.page.html',
  styleUrl: './login.page.scss',
})
export class LoginPage {

  loginModel = signal({
    username: '',
    password: '',
    isPasswordVisible: false
  });

  loginForm = form(this.loginModel);

  protected handleSubmit(event: SubmitEvent) {
    event.preventDefault();
  }
}
