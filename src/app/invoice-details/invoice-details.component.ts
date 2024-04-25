import { Component } from '@angular/core';
import { ServiceService } from '../service.service';
import { Router } from '@angular/router';
import { Product } from '../product';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-invoice-details',
  templateUrl: './invoice-details.component.html',
  styleUrl: './invoice-details.component.css'
})
export class InvoiceDetailsComponent {

  
  departmentId: string|undefined;
  departmentName: string |undefined;
  totalAmount: number |undefined;

  constructor(private serviceService: ServiceService, private dialog: MatDialog, private snackBar: MatSnackBar) { }
  generateInvoice() {
    this.snackBar.open('Invoice generated successfully!', 'Close', {
      duration: 3000, 
    });
  }

  receiveTotalAmount(totalAmount: number) {
    this.totalAmount = totalAmount;
  }

}
