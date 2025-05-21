import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { provideAuth0 } from "@auth0/auth0-angular";

import { AppModule } from './app/app.module';
import {environment} from "./environments/environment";


platformBrowserDynamic().bootstrapModule(AppModule, {
  providers: [
    provideAuth0(environment.auth0)
  ]
})
  .catch(err => console.error(err));
