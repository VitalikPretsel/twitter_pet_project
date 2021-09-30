import { UsersService } from "../_services/users.service";

export function appInitializer(usersService: UsersService) {
    return () => new Promise(resolve => {
        usersService.getCurrentUser()
            .subscribe(() => { }, () => { })
        .add(resolve);
    });
}