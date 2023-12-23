import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { HttpErrorResponse, provideHttpClient } from '@angular/common/http';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { MessageService } from 'primeng/api';

export const appConfig: ApplicationConfig = {
  providers: [
    MessageService,
    provideHttpClient(),
    provideRouter(routes),
    importProvidersFrom(
      [
        BrowserAnimationsModule,
        SocialLoginModule
      ]),
      {
        provide: 'SocialAuthServiceConfig',
        useValue: {
          autoLogin: false,
          providers: [
            {
              id: GoogleLoginProvider.PROVIDER_ID,
              provider: new GoogleLoginProvider(
                '213788433199-c34kpsnki4ugo43sh1k0aaav2nobs8uv.apps.googleusercontent.com'
              )
            },
          ],
          onError: (err:HttpErrorResponse) => {
            console.error(err);
          }
        } as SocialAuthServiceConfig,
      }
    
    
    
    
    
    
    
    
    
    
    ]
};
