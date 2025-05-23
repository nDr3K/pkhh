import { provideAuth0 } from "@auth0/auth0-angular";

import { environment } from "./environments/environment";
import { enableProdMode } from "@angular/core";
import { bootstrapApplication } from "@angular/platform-browser";
import { AppComponent } from "./app/app.component";
import { provideRouter } from "@angular/router";
import { provideHttpClient } from "@angular/common/http";
import { ROUTES } from "./app/routes";


if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(ROUTES),
    provideHttpClient(),
    provideAuth0(environment.auth0)
  ]
}).then();
