export interface InventoryItem {

    id: number;
    tenantId: string;
    name: string;
    description: string;
    price: number;
    quantity: number;
    category: string;
    warehouses: any[];
}
