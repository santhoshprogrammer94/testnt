import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'tnt-auto-login',
  templateUrl: './auto-login.component.html',
  styleUrls: ['./auto-login.component.scss']
})
export class AutoLoginComponent implements OnInit {
  lang: any;

  constructor(public oidcSecurityService: OidcSecurityService
  ) {
    this.oidcSecurityService.onModuleSetup.subscribe(() => { this.onModuleSetup(); });
  }

  ngOnInit() {
    if (this.oidcSecurityService.moduleSetup) {
      this.onModuleSetup();
    }
  }

  ngOnDestroy(): void {
  }

  private onModuleSetup() {
    this.oidcSecurityService.authorize();
  }
  
}
