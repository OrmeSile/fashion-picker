import {Component, OnInit, signal} from '@angular/core';
import {OutfitTagControl} from '../../components/controls/outfit-tag-control/outfit-tag.control';
import {SnowflakeIcon} from '../../components/icons/snowflake.icon/snowflake.icon';
import {SummerSunImageIcon} from '../../components/icons/summer-sun-image.icon/summer-sun-image.icon';
import {SpringFlowerIcon} from '../../components/icons/spring-flower.icon/spring-flower.icon';
import {AutumnPumpkinIcon} from '../../components/icons/autumn-pumpkin.icon/autumn-pumpkin.icon';
import {Outfit} from '../../../types/outfit.types';
import {OutfitApi} from '../../services/api/outfit-api/outfit-api';

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
  outfits = signal<Outfit[]>([]);

  constructor(private outfitApi: OutfitApi) {
  }

  ngOnInit(): void {
    this.outfitApi.getOutfits()
      .subscribe(outfits => this.outfits.set(outfits));
  }
}
