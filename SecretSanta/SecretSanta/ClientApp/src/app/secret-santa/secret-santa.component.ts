import { Component, OnInit, Inject } from '@angular/core'
import { HttpClient } from '@angular/common/http'

@Component({
    selector: 'app-secret-santa',
    templateUrl: './secret-santa.component.html',
    styleUrls: ['./secret-santa.component.css'],
})
export class SecretSantaComponent implements OnInit {
    public santas: SecretSantaPair[]
    private readonly baseUrl: string
    private readonly httpClient: HttpClient

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.httpClient = http
        this.baseUrl = `${baseUrl}api/SecretSanta`
    }

    ngOnInit() {
        this.fetchSantas()
    }

    handleSantaRegistration(userData: { firstName: string; lastName: string }) {
        const id: number = 0
        this.postUser({ id, ...userData })
    }

    postUser(user: User) {
        this.httpClient.post<User>(this.baseUrl, user).subscribe(
            (result) => {
                this.fetchSantas()
            },
            (error) => console.error(error)
        )
    }

    fetchSantas() {
        this.httpClient.get<SecretSantaPair[]>(`${this.baseUrl}/GetSantas`).subscribe(
            (result) => {
                this.santas = result
            },
            (error) => console.error(error)
        )
    }
}

interface User {
    id: number
    firstName: string
    lastName: string
}

interface SecretSantaPair {
    giver: User
    receiver: User
}
