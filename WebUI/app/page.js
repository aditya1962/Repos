"use client"

import styles from "./page.module.css";
import InvoiceData from "./InvoiceData/page.js"

export default function Home() {
  return (
    <form onClick={e => e.preventDefault()}>
      <div className={styles.page}>        
        <InvoiceData />
      </div>
    </form>
  );
}