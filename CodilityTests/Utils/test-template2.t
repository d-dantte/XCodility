<html>
<head>
    <style type="text/css" media="print">
        /* For Printing Page Number */
        #footer {
            display: block;
            position: running(footer);
            color:gray;
            font-size: 50%;
        }
        #header {
            display: block;
            position: running(header);
        }
        #footerText {
            text-align: center;
        }
        #pageNumberFooter {
            text-align: right;
        }
        @page {
            @bottom-center {
                content: element(footer);
            }
            @top-center {
                content: element(header);
            }
            margin-bottom : 1.5in;
            margin-top : 76px;
            size : A4;
        }
        .showTopBorder td {
            border-top: 1px solid black;
        }
        #pageNumber:before {
            content: counter(page);
        }
        #pageCount:before {
            content: counter(pages);
        }
        .floatLeft{
            float: left;
        }
        .floatRight{
            float: right;
        }
        .onlyOuterBorderTable {
            border-collapse:collapse;
            border:1px solid black;
        }
        .onlyOuterBorderTable tr, .onlyOuterBorderTable td {
            border:none;
        }
		tr, td {
			border-collapse:collapse;
		}
        /* For displaying amounts with new rupee symbol */
        .playFair{
            font-family: "Playfair Display";
            font-size: 17px;
        }
    </style>
</head>

<body>
    <div style="font:15px ariel,sans-serif;">
        <!-- Print Page Number -->
<div id="footer">
            #if ( $footerCitations )
                <div id="footerText"> 
                    #set($footerCitationSep = "")
                    #set($footerCitationLineSep = "")
                    #foreach($singleFooterCitation in $footerCitations)
                        #if ( $singleFooterCitation )
                            $footerCitationSep
                            #set($footerCitationSep = "<br/>")
                            #foreach($line in $singleFooterCitation)
                                $footerCitationLineSep$line
                                #set($footerCitationLineSep = "<br/>")
                            #end
                        #end 
                    #end
                </div>
            #end
            <div id="pageNumberFooter">
                Page <span id="pageNumber"/> of <span id="pageCount"/>
            </div>
        </div>
        <br/> <!--breaklines for start of page-->
        <div id="header">
            <table width="100%">
                <tr>
                    <td><img src="$logoDetails.logoName" alt="Amazon Logo" style="display:block;max-width:270px;max-height:76px;height:76px;width:270px;"/></td>
                    <td style="text-align:right; font-size:18px"><b>Credit Note</b>
                    #if ($flavor && $flavor == "ORIGINAL")
                        <br/>(Original for Recipient) 
                    #elseif ($flavor && $flavor == "DUPLICATE")
                        <br/>(Duplicate for Supplier) 
                    #end
                </td>    
                </tr>
            </table>
        </div>
        
        #if($GOVT_STATUS && ($GOVT_STATUS == "GOVT_ACCEPTED" || $GOVT_STATUS == "GOVT_DOWN"))
        <div id="qrCodeBlock" align="right">
            <table width="100%" style="border:none;width:100%;border-collapse:collapse;border-spacing:0px;padding:1px">
                <tr>
                    <td style="font-size:15px">
                        
                        #if($GOVT_STATUS && $GOVT_STATUS == "GOVT_ACCEPTED")
                        	#if($GOVT_QR_CODE_IMAGE)
                        		<b>IRN/QR Code:</b>
                        		<img src="data:image/jpeg;base64,$GOVT_QR_CODE_IMAGE" alt="Qr Image"
                             	style="height:144px;width:144px;padding-left:15px" align="right"/>
                            #end
                        #else
                        	<b>IRN/QR Code:</b>
                        	<b>IRP Down</b>
                        #end
                    </td>
                </tr>
            </table>
        </div>
        #end
        
        <div id="addresses">
            <br/><!-- Breaklines for address window in envelope -->
            <table style="border:none;width:100%;border-collapse:collapse;border-spacing:0px;padding:10px" >    
                <tbody valign="top">
                    <tr>
                        <td width="50%" align="left" style="text-align:left;vertical-align:top;">
                            <div style="font-weight:bold;">
                                Sold By:
                            </div>
                            #if($sellerAddressInfo)
								$sellerAddressInfo.sellerName
							#else
	                			$sellerName
							#end<br/>
                            #if ( $footerCitations )
                               #foreach($singleFooterCitation in $footerCitations)
                                   #if ( $singleFooterCitation )
                                       #foreach($footerline in $singleFooterCitation)
                                          #if ( $footerline.indexOf("ASSPL") != -1 )
                                              <sup>*</sup>
                                          #end
                                       #end
                                   #end 
                              #end
                           #end 
                            #foreach($line in $shipFromAddress)
                                $line<br/>
                            #end
                        </td>
                        <td width="50%" align="right" style="text-align:right;">
                            <div style="font-weight:bold">
                                Shipping Address:
                            </div>
                            #if ($addressRenderDataMap.get("DestinationMailingAddress").contactName) $addressRenderDataMap.get("DestinationMailingAddress").contactName <br/> #end
                            #foreach($line in $addressRenderDataMap.get("DestinationMailingAddress").addressLines)
                                $line<br/>
                            #end
							#if ($shipToGSTNumber)<b>GST Registration No: </b>$shipToGSTNumber<br/>#end
							#if ($shipToPANNumber)<b>PAN No: </b>$shipToPANNumber <br/>#end
							#if ($addressRenderDataMap.get("DestinationMailingAddress").stateCode) 
                            	<b>State/UT Code: </b>
                                $addressRenderDataMap.get("DestinationMailingAddress").stateCode <br/>
                            #end
                        </td>
                    </tr>
                    <tr><td colspan="2"><!--this row is for spacing purposes--><br/></td></tr>
                    <tr>
                        <td width="50%" align="left" style="text-align:left;vertical-align:top;">
                            #if ($sellerPANNumber)
                            <b>PAN No: </b>$sellerPANNumber<br/>
                            #end
                            #if ($sellerGSTNumber)
                            <b>GST Registration No: </b>$sellerGSTNumber<br/>
                            #end
                            #if ($sellerCINNumber)
                            <b>CIN No: </b>$sellerCINNumber<br/>
                            #end
                            #if ($shouldRenderActualShipFrom && !$isFromIndiaDropShip)
                                <br />
                                <br />
                                <div style="font-weight:bold">Ships From :</div>
                                #foreach($line in $addressRenderDataMap.get("ActualShipFromAddress").addressLines)
                                    $line<br/>
                                #end
                                <br />
                            #end
                        </td>
                        <td width="50%" align="right" style="text-align:right;">
                            <div style="font-weight:bold">
                                Billing Address:
                            </div>
                            #if ($addressRenderDataMap.get("BillingAddress").contactName) $addressRenderDataMap.get("BillingAddress").contactName <br/> #end
                            #if ($addressRenderDataMap.get("BillingAddress").addressLines)
                            	#foreach($line in $addressRenderDataMap.get("BillingAddress").addressLines)
                            		$line<br/>
                            	#end
                            #else
                            	#foreach($line in $billingaddress)
                            		$line<br/>
                            	#end
                            #end
							#if ($billToGSTNumber)<b>GST Registration No: </b>$billToGSTNumber<br/>#end
                            #if ($billToPANNumber)<b>PAN No: </b>$billToPANNumber <br/>#end
                            #if ($addressRenderDataMap.get("BillingAddress").stateCode)
                                    <b>State/UT Code: </b> $addressRenderDataMap.get("BillingAddress").stateCode<br/>
                                #end
                                <br/><br/>
                            #if (($billToGSTNumber && $addressRenderDataMap.get("BillingAddress").state) || $addressRenderDataMap.get("DestinationMailingAddress").state)
                                <b>Place of supply: </b> 
                                #if ($billToGSTNumber && $addressRenderDataMap.get("BillingAddress").state)
                                	$addressRenderDataMap.get("BillingAddress").state<br/>
                        	 	#else 
                        	 		$addressRenderDataMap.get("DestinationMailingAddress").state <br/>#end
                        	 #if ($addressRenderDataMap.get("DestinationMailingAddress").state)
                            	<b>Place of delivery: </b>
                            	$addressRenderDataMap.get("DestinationMailingAddress").state<br/>
                            #end
                        	 #end
                        </td>
                    </tr>
                </tbody>
            </table>


        </div>

        <br/>
        <div id="invoice_date_and_identifier">
            <table class="floatLeft" style="width:100%;border-collapse:collapse;border-spacing:0px">
                <tr>
                    <td style="text-align:left"><b>Order No:</b> $orderId</td>
                    <td style="text-align:right"><b>Credit Note No:</b> $invoiceNumber</td>
                </tr>
                <tr>
                    <td style="text-align:left">#if($invoiceNumber)<b>Original Invoice Number:</b> $originalForwardInvoiceNumber #end</td>
                    <td style="text-align:right">#if($invoiceDetails)<b>Invoice Details:</b> $invoiceDetails #end</td>
                </tr>
                <tr>
                    <td style="text-align:left"><b>Original Order Date:</b> $orderDate</td>
                    <td style="text-align:right"><b>Credit Note Date:</b> $invoiceDate</td>
                </tr>
                <tr>
                    <td style="text-align:left" colspan="2">#if($purchaseOrderNumber)<b>PO Number:</b> $purchaseOrderNumber #end</td>
                </tr>
            </table>
        </div>
        <p/>
        <div style="clear:both"></div>
        <table width="100%" style="font-size:80%;-fs-table-paginate: paginate;border-spacing:0px;border-collapse:collapse; border:1px solid black;">
            <thead valign="top" align="left" style="background-color:#CACACA;border-spacing:0px;border-collapse:collapse;border:1px solid black;">
                <tr>
                    <td style="white-space:normal;border-right: 1px solid black" ><b>Sl. No</b></td>
                    <td style="white-space:normal;border-right: 1px solid black"><b>Description</b></td>
                    <td style="white-space:normal;border-right: 1px solid black"><b>Unit Price</b></td>
                    <td style="white-space:normal;border-right: 1px solid black"><b>Quantity</b></td>
                    #if($shouldDisplayPromotions)
                    <td style="white-space:normal;border-right: 1px solid black"><b>Discount</b></td>
                    #end
                    <td style="white-space:normal;border-right: 1px solid black"><b>Net Amount</b></td>
                    <td style="white-space:normal;border-right: 1px solid black"><b>Tax Rate</b></td>
                    <td style="white-space:normal;border-right: 1px solid black"><b>Tax Type</b></td>
                    <td style="white-space:normal;border-right: 1px solid black"><b>Tax Amount</b></td>
                    <td style="white-space:normal"><b>Total Amount</b></td>
                </tr>
            </thead>
            <tbody valign="top" align="left" style="padding-bottom: .5em;border-spacing:0px;border-collapse:collapse;border:1px solid black;">
                #set($prevLineItemId = 0)
                #set($slNo = 0)
                #if($lineItemsMap)
           			#set ($lineItemList = $lineItemsMap.INVOICE_LINE_TYPE)
        		#else
          			#set ($lineItemList = $lineItems)
        		#end
                #foreach($item in $lineItemList)
                  #set($lineItemTaxNo = 0)
                  #foreach($lineItemLevelTax in $item.lineItemLeveltaxes)  
                    #if ($prevLineItemId != $item.lineItemId)
                        #set($showTopBorder = "showTopBorder")
                    #else
                        #set($showTopBorder = "noTopBorder")
                    #end
                    <tr class="$showTopBorder">
                        <td class="playFair" style="white-space:normal;text-align:center;border-right: 1px solid black" valign="top">
                            #if ($prevLineItemId != $item.lineItemId)
                                #set($prevLineItemId = $item.lineItemId)
                                #set($slNo = $slNo + 1)
                                #if ($lineItemTaxNo == 0)
                                   $slNo
                                #end
                            #end
                        </td>
                        <td style="white-space:normal;text-align:left;border-right: 1px solid black">
                            #if($item.skuNumber && $lineItemTaxNo == 0)
                            	$item.description ( $item.skuNumber )
                            #elseif ($lineItemTaxNo == 0)
                            	$item.description
                            #end
                            <br/>
                            #if($item.hsnCode && $lineItemTaxNo == 0) 
                                 HSN:$item.hsnCode 
                        	#elseif($item.sacNumber && $lineItemTaxNo == 0) 
                        	     SAC:$item.sacNumber
                        	#end
                        </td>
                        <td class="playFair" style="white-space:normal;text-align:center;border-right: 1px solid black">
                           #if( $lineItemTaxNo == 0)
                            	$item.unitPrice
                           #end
                        </td>
                        <td class="playFair" style="white-space:normal;text-align:center;border-right: 1px solid black">
                            #if( $lineItemTaxNo == 0)
                        	     $item.quantity
                        	#end
                        </td>
                        #if($shouldDisplayPromotions  )
                          <td class="playFair" style="white-space:normal;text-align:center;border-right: 1px solid black">
                        	 #if( $lineItemTaxNo == 0)
                        	     $item.unitPromoBasisAmount
                        	 #end
                          </td>
                        #end
                        <td class="playFair" style="white-space:normal;text-align:center;border-right: 1px solid black">
                            #if( $lineItemTaxNo == 0)
                             	$item.unitPricePlusPromoBasisAmountParenTimesQuantity
                        	 #end
                        </td>
                        <td class="playFair" style="white-space:normal;text-align:center;border-right: 1px solid black">
                             	$lineItemLevelTax.taxRate
                        </td>
                        <td style="white-space:normal;text-align:center;border-right: 1px solid black">
                        	    $lineItemLevelTax.taxName
                        </td>
                        <td class="playFair" style="white-space:normal;text-align:center;border-right: 1px solid black">
                        	    $lineItemLevelTax.taxAmountPlusPromoTaxAmountTimesQuantity
                        </td>
                    <td class="playFair" style="white-space:normal;text-align:center">
                    	#if( $lineItemTaxNo == 0)
                    	   $item.unitPricePlusPromoBasisAmountPlusUnitTaxAmountPlusPromoTaxAmountParenTimesQuantity
                         #end
                    </td>
                    </tr>
                    #set($lineItemTaxNo = $lineItemTaxNo + 1)
                #end
                   #set($lineItemTaxNo = 0)
               #end
                <tr>
                    <td colspan="#if($shouldDisplayPromotions)8#else 7 #end" style="text-align:left;;border-right: 1px solid black;border-top: 1px solid black"><b>TOTAL:</b></td>
                    <td class="playFair" style="border-top:1px solid black;background-color:#CACACA; text-align:right;border-right: 1px solid black"><b>$totalTaxAmount</b></td>
                    <td class="playFair" style="border-top:1px solid black;background-color:#CACACA; text-align:right"><b>$totalPrice</b></td>
                </tr>
            </tbody>
        </table>
        <table class="onlyOuterBorderTable" style="width:100%;page-break-inside: avoid;">
                <tr><td align="left" style="text-align:left"><b>Amount in Words:</b></td></tr>
                <tr><td align="left" style="text-align:left"><b>$totalPriceInWords</b></td></tr>
        </table>
        <table style="width:100%;border-collapse:collapse;border:1px solid black;page-break-inside: avoid;">
                <tr style="border:1px solid black;" >
                    <td style="text-align:left;color:gray;font-size: 50%;">
                    #if ( $signatureCitations )
                        #foreach($citation in $signatureCitations)
                        <p>$citation<br/></p>
                        #end
                    #end
                    </td>
                    <td style="text-align:right">
                    	<table align="right">
                    		<tr>
                    			<td style="text-align:right">
                    				<b>For 
                    				#if($sellerAddressInfo)
										$sellerAddressInfo.sellerName:</b>
									#else
	                					$sellerName:</b>
									#end
                        		</td>
                        	</tr>
                        	<tr>
                        		<td style="text-align:right">
                        			#if ($signatureImage)
                        			<img src="data:image/jpeg;base64,$signatureImage" alt="Signature Image" style="display:block;max-width:270px;max-height:76px;height:35px;width:170px;" align="right"/>
                        			#else
                        			<br/><br/>
                        			#end
                        		</td>
                        	</tr>
                        	<tr>
                        		<td style="text-align:right">
                        			<b>Authorized Signatory</b>
                        		</td>
                        	</tr>
                        </table>
                    </td>
                </tr>
                <tr style="border:1px solid black;">
                    <td style="text-align:left;color:gray;font-size: 50%;text-align:justify" colspan="2">
                    #if ($lineItemLevelCitations)
                        #foreach($lineItemLevelCitation in $lineItemLevelCitations)
                            <p>
                            ($foreach.count) $lineItemLevelCitation<br/>
                            </p>
                        #end
                    #end
                    </td>
                </tr>
        </table>
        #if($b2bCitation)
                <br/>
                <br/>
              <p>
              ** $b2bCitation
              </p>
        #end
    </div>
</body>
</html>