import {Component, inject} from '@angular/core';
import {OutfitTagControl} from '../../components/controls/outfit-tag-control/outfit-tag.control';
import {LeafIcon} from '../../components/icons/leaf-icon/leaf.icon';
import {AutumnLeafIcon} from '../../components/icons/autumn-leaf.icon/autumn-leaf.icon';
import {SnowflakeIcon} from '../../components/icons/snowflake.icon/snowflake.icon';
import {OutfitStore} from '../../stores/outfit-store/outfit.store';
import {SummerSunImageIcon} from '../../components/icons/summer-sun-image.icon/summer-sun-image.icon';
import {SpringFlowerIcon} from '../../components/icons/spring-flower.icon/spring-flower.icon';
import {AutumnPumpkinIcon} from '../../components/icons/autumn-pumpkin.icon/autumn-pumpkin.icon';

@Component({
  selector: 'fp-main-page',
  imports: [
    OutfitTagControl,
    LeafIcon,
    AutumnLeafIcon,
    SnowflakeIcon,
    SummerSunImageIcon,
    SpringFlowerIcon,
    AutumnPumpkinIcon
  ],
  templateUrl: './main.page.html',
  styleUrl: './main.page.scss',
})
export class MainPage {
  outfitStore = inject(OutfitStore);
}
