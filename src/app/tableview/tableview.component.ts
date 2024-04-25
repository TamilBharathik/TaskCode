import { Component, OnInit,  } from '@angular/core';
import { ServiceService } from '../service.service';
import { Router } from '@angular/router';
import { Product } from '../product';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EventEmitter, Output } from '@angular/core';
import { InvoiceDetailsComponent } from '../invoice-details/invoice-details.component';

interface PageEvent {
  first: number; 
  rows: number;
  page: number;
  pageCount: number;
} 

@Component({
  selector: 'app-tableview',
  templateUrl: './tableview.component.html',
  styleUrl: './tableview.component.css'
})
export class TableviewComponent implements OnInit {
  // @Output() totalAmountEvent = new EventEmitter<number>();

  products: Product[] = [];
  filteredProducts: Product[] = [];
  selectedInvoicePeriod: string | null = null; 
  visible: boolean = false;
  totalAmount: number | undefined;
  showDialog() {
      this.visible = true;
  }
  
  constructor(private ServiceService:ServiceService, private router: Router, private snackBar: MatSnackBar
   
  ){

  }
  ngOnInit() {
    this.ServiceService.getAll().subscribe((data) => {
        this.products = data;
        this.applyFilter();
    });
}

applyFilter(): void {
  if (!this.selectedInvoicePeriod) {
    this.filteredProducts = this.products;
  } else {
    this.filteredProducts = this.products.filter(product => product.invoicePeriod === this.selectedInvoicePeriod);
  }

  this.totalAmount = this.filteredProducts.reduce((total, product) => {
    return total + (product.estimateCost || 0); 

    
  }, 0);

  // this.totalAmountEvent.emit(this.totalAmount);

  
}

generateInvoice1() {
  this.snackBar.open('Invoice generated successfully!', 'Close', {
    duration: 15000, 
  });
}

receiveTotalAmount(totalAmount: number) {
  this.totalAmount = totalAmount;
}




first: number = 0;
rows: number = 10;

onPageChange(event: PageEvent) {
  this.first = event.first || 0;
  this.rows = event.rows || 10;
}


generateInvoice() {
  this.snackBar.open('Invoice generated successfully!', 'Close', {
    duration: 3000,
  });


}

}