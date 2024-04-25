export interface Product {
    productID: number,
    productName: String,
    country: string,
    invoicePeriod: string,
    scrapType: string,
    manCost: number,
    materialCost: number,
    estimateCost: number|null,
    localAmount: number,
    image: File | undefined
}

export interface Country{
    country: string
    currency:number
}
