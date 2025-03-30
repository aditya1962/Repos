import { useEffect, useState } from "react"
import axios from 'axios';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

let productApi = "http://localhost:5114/api/v1/product/get-productlist";
let invoiceApi = "http://localhost:5114/api/v1/invoice/generate-invoice";

export default function InvoiceData(){
    let [productData, setProductData] = useState([{product:"",unitPrice:0.0, quantity:0,discount:0,total:0}]);
    const [subTotal, setSubTotal] = useState(0.0);
    const [productValues,setValues] = useState([]);
    const [transactionDate, setTransactionDate] = useState(new Date());
    const [payment, setPayment] = useState(0.0);

    function handleValue(e,i){
        let newFormValues = [...productData];
        let editValue = newFormValues[i];
        editValue[e.target.name] = e.target.value;
        editValue.total = (editValue.unitPrice*editValue.quantity) - editValue.discount;
        setTotal();
        setProductData(newFormValues);
    }

    function setTotal(){
        setSubTotal(productData.reduce(
            (accumulator, currentValue) => accumulator + currentValue.total,
            0,
          )); 
    }

    function addRow(){
        setProductData([...productData,{product:"",unitPrice:0, quantity:0,discount:0,total:0}]);
    }

    function removeRow(id){
        productData = productData.filter(a => a.id != id);
        setTotal();
        setProductData(productData);       
    }

    function submitData(){
        for(var i=0; i < productData.length; i++)
        {
            if(productData[i].quantity <= 0 )
            {
                alert("Please fill up quantity field");
                break;
            }
        }

        let data = {"TransactionDate":transactionDate,
                    "Total":subTotal,
                    "ProductData":productData,
                    "Balance": subTotal-payment};
                    
        axios.post(invoiceApi, data).then(res => {
            if(res.status = 201) alert("Created invoice successfully");
        }).catch(error => {
            if(error.response.status && error.response.status===400)
                alert("Bad Request");
            else alert("Something Went Wrong");
        });
    }

    function handleProduct(e,i){       
        let newFormValues = [...productData];
        let editValue = newFormValues[i];
        editValue["product"] = e.target.value;
        editValue["unitPrice"] = productValues[productValues.findIndex((value) => value.product == e.target.value)].unitPrice;
        setTotal();
        setProductData(newFormValues);
    }

    useEffect(() => {
        async function fetchData() {
            axios.get(productApi).then(res => {
                setValues(res.productData);
                }).catch(error => {
                    alert("Could not load items");
            })
        }
        fetchData()
    },[])

    return(
    <div>
        <div className="row">
            <div className="col-md-2">
                <label>Transaction Date</label>
            </div>
            <div className="col-md-10">
                <DatePicker selected={transactionDate} onChange={(date) => setTransactionDate(date)} />
            </div>            
        </div>
        <div className="row header">
            <div className="col-md-3">
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
            <div className="col-md-1">
                <label>Amount</label>
            </div>
        </div>
        {productData.map((val,index) => (
            <div className="row  header" key={index}>
                <div className="col-md-3">
                    <select id="productlist" value={productData[index].product} onChange={e => {handleProduct(e,index)}}>
                        <option key= "-1" value=""></option>
                        {productValues.map((value,index) => (
                            <option key= {index} value={value.product}>{value.product}</option>
                        ))}
                    </select>
                </div>
                <div className="col-md-2">
                    <label name="unitPrice" onChange={e => handleValue(e,index)}>{val.unitPrice}</label>
                </div>
                <div className="col-md-2">
                    <input type="number" className="form-control" name="quantity" value={val.quantity} onChange={e => handleValue(e,index)}></input>
                </div>
                <div className="col-md-2">
                    <input type="number" className="form-control" name="discount" value={val.discount} onChange={e => handleValue(e,index)}></input>
                </div>
                <div className="col-md-2">
                    <label name="amount">{(val.unitPrice*val.quantity) - val.discount}</label>
                </div>
                <div className="col-md-1 rt">
                    <button className="btn btn-success" onClick = {addRow}>+</button>
                    <button className="btn btn-danger" onClick = {() => {removeRow(index)}}>-</button>
                </div>
            </div>
        ))}
        <div className="row">
            <div className="col-md-1 offset-md-10 rt">
                <label>Total</label>
            </div>
            <div className="col-md-1">
                <label>{subTotal}</label>
            </div>
        </div>
        <div className="row">
            <div className="col-md-1 offset-md-10 rt">
                <label>Amount paid</label>
            </div>
            <div className="col-md-1">
                <input type="number" className="form-control" name="paid" value={payment} onChange={e => setPayment(e.target.value)}></input>
            </div>
        </div>
        <div className="row">
            <div className="col-md-1 offset-md-10 rt">
                <label>Balance</label>
            </div>
            <div className="col-md-1">
                <label>{subTotal-payment}</label>
            </div>
        </div>
        <div className="row">
            <div className="col-md-2 offset-md-10 rt">
                <button className="btn btn-primary" onClick = {submitData}>Generate Invoice</button>
            </div>
        </div>
    </div>
    )
}