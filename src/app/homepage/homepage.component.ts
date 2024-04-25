import { COMPILER_OPTIONS, Component, OnInit } from '@angular/core';
import { DropdownFilterOptions } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ServiceService } from '../service.service';
import { Router } from '@angular/router';
import { Product } from '../product';
import { Country } from '../product';
import { Directive, ElementRef} from '@angular/core';
import { FileUpload, FileUploadEvent } from 'primeng/fileupload';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TableviewComponent } from '../tableview/tableview.component';

interface UploadEvent {
    originalEvent: Event;
    files: File[];
}



@Component({
    selector: 'app-homepage',
    templateUrl: './homepage.component.html',
    styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
    

    isButtonDisabled: boolean = true;
    isDuplicate: boolean = false;


    addproductRequest: Product = {
        productID: 0,
        productName: '',
        country: '',
        invoicePeriod: '',
        scrapType: '',
        manCost: 0,
        materialCost: 0,
        estimateCost: null,
        localAmount: 0,
        image: undefined
    };




   

    formSubmitted = false;
    // selectedCountry: string = ''; // Initialize selectedCountry
    // countries = [
    //     { id: 1, name: 'India' },
    //     { id: 2, name: 'Australia' },
    //     { id: 3, name: 'united States' },
    //     { id: 4, name: 'swedan' },
    // ];

    invoicePeriod: string = '';
    periods = [
        { id:1, name: 'Weekly' },
        { id:2, name: 'Fortnight' },
        { id:3, name: 'Monthly' },
        { id:4, name: 'Half-Yearly' },
        { id:5, name: 'Yearly' }
    ];

    scrapType: string = ''; 
    scraptypes = [
        { name: 'Ferrous' },
        { name: 'Non-Ferrous' }
    ];

    filterValue: string | undefined = '';
    countries: Country[] = []; 
    selectedCountry: string='';
    selectedFile!: File | Blob ;

    constructor(
        private ServiceService: ServiceService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService,
        private router: Router,
        private el: ElementRef,
        private http: HttpClient
    ) {
        if (!el.nativeElement.querySelector('.required-asterisk')) {
            const asteriskElement = document.createElement('span');
            asteriskElement.className = 'required-asterisk';
            asteriskElement.style.color = 'red';
            // asteriskElement.innerText = '*';
            el.nativeElement.appendChild(asteriskElement);
          }
    }
   
    ngOnInit() { 
        this.getCountries(); 
    }
   
    onUpload(event: FileUploadEvent) {
        this.messageService.add({ severity: 'info', summary: 'Success', detail: 'File Uploaded with Basic Mode' });
    }

    createTask() {
        this.addproductRequest.invoicePeriod = this.invoicePeriod;
        this.addproductRequest.scrapType = this.scrapType;
        this.addproductRequest.country = this. selectedCountry;
        console.log(JSON.stringify(this.addproductRequest));
        
        this.ServiceService.addProduct(this.addproductRequest)
            .subscribe({
                next: (response) => {
                    console.log('Response from backend:', response);
                    // Handle the response based on your application's requirements
                },
                error: (error) => {
                    console.error('Error adding product:', error);
                    // Handle the error appropriately, e.g., display an error message to the user
                }
            });
    }
    calculateEstimateCost() {
        if (this.addproductRequest.manCost != null && this.addproductRequest.materialCost != null) {
            
            this.addproductRequest.estimateCost = this.addproductRequest.manCost + this.addproductRequest.materialCost;
            console.log(JSON.stringify(this.countries));
            this. countries.forEach(count =>{
              if(this.selectedCountry===count.country){

                this.addproductRequest.localAmount = (this.addproductRequest.estimateCost != null ? this.addproductRequest.estimateCost : 0) * count.currency;

              }
            });
            
        } else {
            this.addproductRequest.estimateCost = null;
        }
    }
    
    
    
    onSubmit(): void {
        
        this.formSubmitted = true;
      
    }

    resetFunction(options: DropdownFilterOptions) {
        if (options && options.reset) {
            options.reset();
        }
        this.filterValue = '';
    }

    customFilterFunction(event: KeyboardEvent, options: DropdownFilterOptions) {
        if (options && options.filter) {
            options.filter(event);
        }
    }

    onBasicUpload(): void {
        this.messageService.add({ severity: 'info', summary: 'Success', detail: 'Estimate Created' });
    }

    getCountries(): void {
        this.ServiceService.getCountries()
          .subscribe(countries => this.countries = countries);
      }


      onFileSelected(event:any): void {
        this.selectedFile = event.target.files[0];
      }
    
      onSubmitImage(): void {
        const reader = new FileReader();
        // Read file as array buffer
        reader.readAsArrayBuffer(this.selectedFile);
        reader.onload = () => {
          if (reader.result instanceof ArrayBuffer) {
            // Convert array buffer to Blob
            const blob = new Blob([reader.result], { type: this.selectedFile.type });
            this.uploadImage(blob);
          }
        };
      }
    
      uploadImage(blob: Blob): void {
        const formData = new FormData();
        if (this.selectedFile instanceof File) {
            formData.append('image', blob, this.selectedFile.name);
          }
        this.http.post('/api/upload', formData).subscribe(
          (response: any) => console.log(response), // Specify type of 'response'
          (error: HttpErrorResponse) => console.log(error) // Specify type of 'error'
        );
      }
    }

