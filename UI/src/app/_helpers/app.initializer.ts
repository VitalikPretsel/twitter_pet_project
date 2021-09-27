import { AuthenticationService } from "../_services/authentication.service";
import { UsersService } from "../_services/users.service";

export function appInitializer(
    authenticationService: AuthenticationService,
    usersService: UsersService) {
    return () => new Promise(resolve => {
        authenticationService.refreshToken()
            .subscribe(() => { }, () => { });
        authenticationService.isAuthenticated()
            .subscribe(async res => {
                if (res) {
                    await usersService.getCurrentUser().toPromise();
                }
            }).add(resolve);
    });
}