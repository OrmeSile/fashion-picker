import {Component, inject, OnInit, signal} from '@angular/core';
import {OutfitTagControl} from '../../components/controls/outfit-tag-control/outfit-tag.control';
import {SnowflakeIcon} from '../../components/icons/snowflake.icon/snowflake.icon';
import {SummerSunImageIcon} from '../../components/icons/summer-sun-image.icon/summer-sun-image.icon';
import {SpringFlowerIcon} from '../../components/icons/spring-flower.icon/spring-flower.icon';
import {AutumnPumpkinIcon} from '../../components/icons/autumn-pumpkin.icon/autumn-pumpkin.icon';
import {Outfit} from '../../../types/outfit.types';
import {OutfitApi} from '../../services/api/outfit-api/outfit-api';
import {UUID} from '../../../types/shared.types';
import {Router} from '@angular/router';
import {AuthApi} from '../../services/api/auth-api/auth-api';
import {UserStore} from '../../stores/user-store/user.store';
import {User} from '../../../types/User.types';

@Component({
  selector: 'fp-main-page',
  imports: [
    OutfitTagControl,
    SnowflakeIcon,
    SummerSunImageIcon,
    SpringFlowerIcon,
    AutumnPumpkinIcon
  ],
  templateUrl: './main.page.html',
  styleUrl: './main.page.scss',
})
export class MainPage implements OnInit {

  private router = inject(Router);
  private authApi = inject(AuthApi);
  protected userStore = inject(UserStore);
  private outfitApi = inject(OutfitApi);
  outfits = signal<Outfit[]>([]);

  ngOnInit(): void {
    this.authApi.getUserInformation()
      .subscribe(res => this.userStore.dispatch({type: 'SET_USER', payload: res}));
    this.outfitApi.getOutfits()
      .subscribe(outfits => this.outfits.set(outfits));
  }

  protected openOutfitEditor(id: UUID) {
    void this.router.navigate(['/outfit', id]);
  }
}
