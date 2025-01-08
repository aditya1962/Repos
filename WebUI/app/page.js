"use client"

import Image from "next/image";
import styles from "./page.module.css";
import InvoiceData from "./InvoiceData/page.js"
//import DatePicker from 'react-datepicker'

export default function Home() {
  return (
    <form onClick={e => e.preventDefault()}>
      <div className={styles.page}>
        <div className="row">
          <div className="col-md-1">
            <label>Transaction date:</label>
          </div>
          <div className="col-md-11">
            
          </div>
        </div>
        <InvoiceData />
      </div>
    </form>
  );
}
