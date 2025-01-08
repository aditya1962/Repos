import { useState } from "react"

let total = 0;

export default function InvoiceData(){
    const [data, setData] = useState([{id:0, product:"",unitPrice:1.0, quantity:0,discount:0},
                                      {id:1, product:"",unitPrice:2.0, quantity:0,discount:0}
                                    ]);
    const [product, setProduct] = useState("");
    const [unitPrice, setUnitPrice] = useState(0.0);
    const [quantity, setQuantity] = useState(0);
    const [discount, setDiscount] = useState(0.0);
    const [dataFiltered, setDataFiltered] = useState({});
    const [total,setTotal] = useState(0.0);

   
    function handleEdit(id){       
        setDataFiltered(data.filter(a => a.id === id)[0]);
        const value = data.map((a,i) => 
            {
                if(i===dataFiltered.id)
                {
                    
                    return {id:i,product:product,unitPrice:unitPrice, quantity:quantity,discount:discount}
                }
                else
                {
                    return a
                }
            }
        )
        setData(value);
        setTotal(data.reduce((a,c) => a + ((c.unitPrice*c.quantity) - c.discount), 0));                   
    }

    return(
    <div>
        <div className="row">
            <div className="col-md-4">
                <label>Product name</label>
            </div>
            <div className="col-md-2">
                <label>Unit Price</label>
            </div>
            <div className="col-md-2">
                <label>Quantity</label>
            </div>
            <div className="col-md-2">
                <label>Discount</label>
            </div>
            <div className="col-md-2">
                <label>Amount</label>
            </div>
        </div>
        {data.map(val => (
            <div className="row" key={val.id}>
                <div className="col-md-3">
                    <textarea className="form-control" name="product" value={val.product} onChange={e => setProduct(e.target.value)}></textarea>
                </div>
                <div className="col-md-2">
                    <label name="unitPrice" onChange={e => setUnitPrice(e.target.value)}>{val.unitPrice}</label>
                </div>
                <div className="col-md-2">
                    <input type="text" className="form-control" name="quantity" value={val.quantity} onChange={e => setQuantity(e.target.value)}></input>
                </div>
                <div className="col-md-2">
                    <input type="text" className="form-control" name="discount" value={val.discount} onChange={e => setDiscount(e.target.value)}></input>
                </div>
                <div className="col-md-2">
                    <label name="amount">{(val.unitPrice*val.quantity) - val.discount}</label>
                </div>
                <div className="col-md-1">
                    <button className="btn btn-success">+</button>
                    <button className="btn btn-danger">-</button>
                    <button id ={val.id} onClick={ () => {handleEdit(val.id)}}>Update</button>
                </div>
            </div>
        ))}
        <div className="row">
            <div className="col-md-1 offset-md-10">
                <label>Total</label>
            </div>
            <div className="col-md-1">
                <label>{total}</label>
            </div>
        </div>
    </div>
    )
}