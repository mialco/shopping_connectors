select pr.productId,  count(pv.VariantID) as prcount
from product pr
inner join ProductVariant pv on pr.ProductID=pv.ProductID
group by pr.ProductID
--having count(pv.VariantID) > 1
